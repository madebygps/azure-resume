#!/bin/bash

# Import the public repository GPG keys
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg

# Move the GPG key to the appropriate directory
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg

# Set up the stable repository
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/debian/$(lsb_release -rs | cut -d'.' -f 1)/prod $(lsb_release -cs) main" > /etc/apt/sources.list.d/dotnetdev.list'

# Update the package lists for upgrades and new packages
sudo apt-get update

# Install the Azure Functions Core Tools
sudo apt-get install azure-functions-core-tools-4