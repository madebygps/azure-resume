az functionapp config appsettings set -g cloud-resume-challenge-rg -n AzureResumeVisitorCounter --settings 'WEBSITE_USE_PLACEHOLDER_DOTNETISOLATED=1'
az functionapp config set -g cloud-resume-challenge-rg -n AzureResumeVisitorCounter --net-framework-version 
"v8.0"
az functionapp config set -g cloud-resume-challenge-rg -n AzureResumeVisitorCounter --use-32bit-worker-process false
