#login with Azure
az login

# create a resource group
$location = "westeurope"
$resourceGroup = "SampleProject"
az group create -l $location -n $resourceGroup

# create an app service plan
$planName="SampleProjectPlan"
az appservice plan create -n $planName -g $resourceGroup -l $location --sku B1

# create a web app
$appName="TestWebApiCoreApp"
az webapp create -n $appName -g $resourceGroup --plan $planName

# deploy from GitHub
$gitrepo="https://github.com/Aleskey/SampleProject.git"
az webapp deployment source config -n $appName -g $resourceGroup `
    --repo-url $gitrepo --branch master --manual-integration

# launch the website in a browser
$site = az webapp show -n $appName -g $resourceGroup --query "hostNames" -o tsv
Start-Process https://$site/swagger