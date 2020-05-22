Write-Host ""
Write-Host "Applying specs..."

foreach ($spec in Get-ChildItem "*.yml") {
    kubectl.exe apply -f $spec
}

kubectl get all
