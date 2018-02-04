using		BxS_SAPNCO.API.Function;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxS_SAPNCO.API.SAPFunctions.BDC
{
	internal class DTO_FNCProfile : RfcFncProfileBase
		{
			#region "Constructors"

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal DTO_FNCProfile( string functionName) : base(functionName)
					{
					}

			#endregion

			//===========================================================================================
			#region "Properties"

				internal	int	TabMsg	{ get; }

				internal	int TabMsg_TCode	{ get; }

				//	TCODE		1	Types	BDC_TCODE		CHAR	 20	0	BDC Transaction code
				//	DYNAME	1 Types	BDC_MODULE	CHAR	 40	0	Batch input module name
				//	DYNUMB	1 Types	BDC_DYNNR		CHAR	  4	0	Batch input screen number
				//	MSGTYP	1 Types	BDC_MART		CHAR	  1	0	Batch input message type
				//	MSGSPRA	1 Types	BDC_SPRAS		LANG	  1	0	Language ID of a message
				//	MSGID		1 Types	BDC_MID			CHAR	 20	0	Batch input message ID
				//	MSGNR		1 Types	BDC_MNR			CHAR	  3	0	Batch input message number
				//	MSGV1		1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
				//	MSGV2		1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
				//	MSGV3		1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
				//	MSGV4		1 Types	BDC_VTEXT1	CHAR	100	0	Variable part of a message
				//	ENV			1 Types	BDC_AKT			CHAR		4	0	Batch input monitoring activity
				//	FLDNAME	1 Types	FNAM_____4	CHAR	132	0	Field name

			#endregion

		}
}
