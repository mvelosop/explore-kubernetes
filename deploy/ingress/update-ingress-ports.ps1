kubectl patch service --type merge ingress-nginx-controller -n ingress-nginx --patch "$(Get-Content ingress-controller-lb-ports.yml -Raw)"
