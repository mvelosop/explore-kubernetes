# explore-kubernetes

Repo to explore Kubernetes

This repo uses the base "application" developed in the [explore-docker-vscode repo](https://github.com/mvelosop/explore-docker-vscode).

- [Overview](#overview)
- [Details](#details)
  - [1 - Run in local Kubernetes](#1---run-in-local-kubernetes)
  - [Install NGINX ingress controller](#install-nginx-ingress-controller)
- [Resources](#resources)

## Overview

## Details

### 1 - Run in local Kubernetes

### Install NGINX ingress controller

- **Kubernetes NGINX Ingress Controller** \
  <https://kubernetes.github.io/ingress-nginx/>

- **Installation guide** \
  <https://kubernetes.github.io/ingress-nginx/deploy/>

```powershell
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-0.32.0/deploy/static/provider/cloud/deploy.yaml
```

## Resources

- **Kubernetes - Deployment** \
  <https://kubernetes.io/docs/concepts/workloads/controllers/deployment/>

