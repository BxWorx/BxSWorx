(function () {
		"use strict";

		var cellToHighlight;
		var messageBanner;

		// The initialize function must be run each time a new page is loaded.
		Office.initialize = function (reason) {
				$(document).ready(function () {
						// Initialize the FabricUI notification mechanism and hide it
						var element = document.querySelector('.ms-MessageBanner');
						messageBanner = new fabric.MessageBanner(element);
						messageBanner.hideBanner();
						
						// If not using Excel 2016, use fallback logic.
						if (!Office.context.requirements.isSetSupported('ExcelApi', '1.1')) {
								$("#template-description").text("This sample will display the value of the cells that you have selected in the spreadsheet.");
								$('#button-text').text("Display!");
								$('#button-desc').text("Display the selection");

								$('#highlight-button').click(displaySelectedCells);
								return;
						}

						$("#template-description").text("This sample highlights the highest value from the cells you have selected in the spreadsheet.");

						$('#button-text').text("Highlight!");
						$('#button-desc').text("Highlights the largest number.");

						$('#sapnco-text').text("Use SAPNCO");
						$('#sapnco-desc').text("Test SAPNCO call");
								
						loadSampleData();

						// Add a click event handler for the highlight button.
						$('#highlight-button').click(hightlightHighestValue);
						$('#sapnco-button').click(GetWSData);
				});
		};

		function loadSampleData() {
				var values = [
						[Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000)],
						[Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000)],
						[Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000), Math.floor(Math.random() * 1000)]
				];

				// Run a batch operation against the Excel object model
				Excel.run(function (ctx) {
						// Create a proxy object for the active sheet
						var sheet = ctx.workbook.worksheets.getActiveWorksheet();
						// Queue a command to write the sample data to the worksheet
						sheet.getRange("B3:D5").values = values;

						// Run the queued-up commands, and return a promise to indicate task completion
						return ctx.sync();
				})
				.catch(errorHandler);
		}

		//.............................................................................................
		function sapncoSystemList()
		{
			$('.disable-while-sending').prop('disabled', true);

			$.ajax
				(
					{
						url: '../../api/GetSystemList',
						type: 'GET',
						//data: JSON.stringify(dataToPassToService),
						contentType: 'application/json;charset=utf-8'
					}
				).done(function (data)
					{
						app.showNotification(data.Status, data.Message);
					}
				).fail(function (status) {
					app.showNotification('Error', 'Could not communicate with the server.');
					}
					)
				.always(function ()
									{
										$('.disable-while-sending').prop('disabled', false);
									}
								);
		}

		//.............................................................................................
		function GetWSData() {
			// Run a batch operation against the Excel object model
			Excel.run(
				function (ctx) {




					var lc_WB = ctx.workbook.load("name")
					var lo_WS = ctx.workbook.worksheets.getActiveWorksheet();
					var lo_UR = lo_WS.getUsedRangeOrNullObject().load("values, rowCount, columnCount, address");

					return ctx.sync()
						.then(function () {

							if (lo_UR !== null) {
								var t;
								for (var r = 0; r < lo_UR.rowCount; r++) {
									for (var c = 0; c < lo_UR.columnCount; c++) {
										t = t + lo_UR.values[r][c];
									}
								}
							}
						}).then(ctx.sync);

				}
			).catch(errorHandler);
		}
		//.............................................................................................
		function hightlightHighestValue() {
				// Run a batch operation against the Excel object model
				Excel.run(function (ctx) {
						// Create a proxy object for the selected range and load its properties
						var sourceRange = ctx.workbook.getSelectedRange().load("values, rowCount, columnCount");

						// Run the queued-up command, and return a promise to indicate task completion
						return ctx.sync()
								.then(function () {
										var highestRow = 0;
										var highestCol = 0;
										var highestValue = sourceRange.values[0][0];

										// Find the cell to highlight
										for (var i = 0; i < sourceRange.rowCount; i++) {
												for (var j = 0; j < sourceRange.columnCount; j++) {
														if (!isNaN(sourceRange.values[i][j]) && sourceRange.values[i][j] > highestValue) {
																highestRow = i;
																highestCol = j;
																highestValue = sourceRange.values[i][j];
														}
												}
										}

										cellToHighlight = sourceRange.getCell(highestRow, highestCol);
										sourceRange.worksheet.getUsedRange().format.fill.clear();
										sourceRange.worksheet.getUsedRange().format.font.bold = false;

										// Highlight the cell
										cellToHighlight.format.fill.color = "orange";
										cellToHighlight.format.font.bold = true;
								})
								.then(ctx.sync);
				})
				.catch(errorHandler);
		}

		function displaySelectedCells() {
				Office.context.document.getSelectedDataAsync(Office.CoercionType.Text,
						function (result) {
								if (result.status === Office.AsyncResultStatus.Succeeded) {
										showNotification('The selected text is:', '"' + result.value + '"');
								} else {
										showNotification('Error', result.error.message);
								}
						});
		}

		// Helper function for treating errors
		function errorHandler(error) {
				// Always be sure to catch any accumulated errors that bubble up from the Excel.run execution
				showNotification("Error", error);
				console.log("Error: " + error);
				if (error instanceof OfficeExtension.Error) {
						console.log("Debug info: " + JSON.stringify(error.debugInfo));
				}
		}

		// Helper function for displaying notifications
		function showNotification(header, content) {
				$("#notification-header").text(header);
				$("#notification-body").text(content);
				messageBanner.showBanner();
				messageBanner.toggleExpansion();
		}
})();
