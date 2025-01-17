﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Module7_WCF
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class CostService
	{
		// To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
		// To create an operation that returns XML,
		//     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
		//     and include the following line in the operation body:
		//         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
		[OperationContract]
		public void DoWork()
		{
			// Add your operation implementation here
			return;
		}

		[OperationContract]
		public double CostOfSandwiches(int quantity)
		{
			return 1.25 * quantity;
		}
	}
}
