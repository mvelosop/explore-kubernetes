param (
    # chart list
    [Parameter(Mandatory = $false)][string[]]$charts
)

$app = "weatherapp"
$chartsFolder = "charts"

if ("$charts" -eq "") {
    Write-Output ""
    Write-Output "Uninstalling Helm charts..."
    helm delete $(helm list -qf "$app-")
} 
else {
    $chartArray = $charts -split ","
    ForEach ($chart in $chartArray) {
        Write-Output ""
        Write-Output "Uninstalling chart ""$chart""..."
        helm delete $(helm list -qf $app-$chart)
    }
}

if ("$charts" -eq "") {
    $chartArray = Get-ChildItem -Directory .\$chartsFolder
} 

ForEach ($chart in $chartArray) {
    Write-Output ""
    Write-Output "Installing chart ""$chart""..."
    helm install $app-$chart $chartsFolder\$chart
}

# install charts

Write-Output ""
Write-Output "Helm charts deployed"
helm list

Write-Output ""
Write-Output "Pod status"
kubectl get pods
