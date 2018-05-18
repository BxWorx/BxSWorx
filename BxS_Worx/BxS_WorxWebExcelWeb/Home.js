'use strict';

(function () {
	Office.initialize = function (reason) {
		$(document).ready(function () {
			$('#set-color').click(setColor);
		});
	};

	function setColor() {
		Excel.run(function (context) {
			var range = context.workbook.getSelectedRange();
			range.format.fill.color = 'green';

			return context.sync();
		}).catch(function (error) {
			console.log("Error: " + error);
			if (error instanceof OfficeExtension.Error) {
				console.log("Debug info: " + JSON.stringify(error.debugInfo));
			}
		});
	}
})();