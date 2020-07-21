$app = "weatherapp"
$chartsFolder = "charts"
$service = "webapp"

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder.UseSerilog $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder.ConfigureWebHostDefaults $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=Startup $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=ConfigureServices $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=Configure $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service $chartsFolder\$service

$service = "webapi"

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder.UseSerilog $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder.ConfigureWebHostDefaults $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=CreateHostBuilder $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=Startup $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=ConfigureServices $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service --set startupException=Configure $chartsFolder\$service

Start-Sleep -Seconds 10

helm delete $app-$service
helm install $app-$service $chartsFolder\$service
