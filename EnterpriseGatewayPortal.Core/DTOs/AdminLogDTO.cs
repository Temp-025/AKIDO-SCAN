using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
	public class AdminLogDTO
	{
		public int Id { get; set; }

		public string? ModuleName { get; set; }

		public string? ServiceName { get; set; }

		public string? ActivityName { get; set; }

		public string? UserName { get; set; }

		public string? DataTransformation { get; set; }

		public string? LogMessageType { get; set; }

		public string? LogMessage { get; set; }

		public string? Checksum { get; set; }

		public string? Timestamp { get; set; }

		public string? Identifier { get; set; }
		public string? IdentifierName { get; set; }

    }
}
