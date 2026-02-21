using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SaveNewDocumentTemplateDTO
{
    public IFormFile File { get; set; }
    public string Model { get; set; }
}

public class TemplateModelDTO
{
    public DocumentTemplateModel docConfig { get; set; }

    public List<TemplateRole> roles { get; set; }

    public string rolesConfig { get; set; }
}

public class DocumentTemplateModel
{
    public string name { get; set; }

	public string docType { get; set; }

	public string documentName { get; set; }

    public string advancedSettings { get; set; }

    public string daysToComplete { get; set; }

    public string numberOfSignatures { get; set; }

    public bool allSigRequired { get; set; }

    public bool publishGlobally { get; set; }

    public bool sequentialSigning { get; set; }

}

public class RoleDetails
{
    public string email { get; set; }

    public TemplateRole role { get; set; }

    public string annotationsList { get; set; }

    public placeHolderCoordinates placeHolderCoordinates { get; set; }

    public esealplaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }
}

public class UpdatedRoleDetail
{
    public string roleId { get; set; }
    public string email { get; set; }
    public TemplateRole role { get; set; }
    public string annotationsList { get; set; }
    public placeHolderCoordinates placeHolderCoordinates { get; set; }
    public esealplaceHolderCoordinates esealPlaceHolderCoordinates { get; set; }
}

public class TemplateRole
{
    public string email { get; set; }

    public string name { get; set; }

    public string description { get; set; }
}