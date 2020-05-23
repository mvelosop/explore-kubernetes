# change the ports in the ingress-ports.yml file

kubectl patch service --type merge ingress-nginx-controller -n ingress-nginx --patch "$(Get-Content ingress-ports.yml -Raw)"
