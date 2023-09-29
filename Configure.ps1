$TenantID="174417d5-e4b0-4ed3-a0ae-ba500ff099b6"
$DisplayNameOfMSI="RestoringPride.org Verifiable Credentials Service"

$ApiAppId = "a0d2437c-a7db-4c11-9904-0e80616b4b54"
$PermissionName = "VerifiableCredential.Create.All"


# Install the module
Install-Module AzureAD

Connect-AzureAD -TenantId $TenantID

$MSI = (Get-AzureADServicePrincipal -Filter "displayName eq '$DisplayNameOfMSI'")

Start-Sleep -Seconds 10

$ApiServicePrincipal = Get-AzureADServicePrincipal -Filter "appId eq '$ApiAppId'"
$AppRole = $ApiServicePrincipal.AppRoles | Where-Object {$_.Value -eq $PermissionName -and $_.AllowedMemberTypes -contains "Application"}
New-AzureAdServiceAppRoleAssignment -ObjectId $MSI.ObjectId -PrincipalId $MSI.ObjectId ` -ResourceId $ApiServicePrincipal.ObjectId -Id $AppRole.Id
