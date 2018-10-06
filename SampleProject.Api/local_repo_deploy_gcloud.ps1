gcloud components update
gcloud auth login

$pathScript = split-path -parent $MyInvocation.MyCommand.Definition
$projectId="sample-project-201810061112"
$projectName="SampleProjectTest201810061112"

gcloud projects create $projectId --name=$projectName
gcloud config set project $projectId
gcloud app create --project=$projectId --region=europe-west3
gcloud endpoints services deploy $pathScript\openapi.yaml

$serviceName=gcloud endpoints configs list --service=$projectId.appspot.com --format="value(SERVICE_NAME)"
$fileBody = "runtime: aspnetcore{0}env: flex{0}endpoints_api_service:{0}  name: {1}{0}  rollout_strategy: managed" -f ([Environment]::NewLine),$serviceName
New-Item -Path $pathScript -Name "app.yaml" -ItemType "file" -Value $fileBody -Force

dotnet restore
dotnet bild
dotnet publish

Write-Host -NoNewLine 'Please enable billing for'$projectName' project, after that press any key to continue';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');

gcloud app deploy $pathScript\bin\Debug\netcoreapp2.1\publish\app.yaml

Start-Process https://$projectId.appspot.com/swagger