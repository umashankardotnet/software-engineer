# Deploy this web api with Docker
- Ref: [Docker Images for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0)
- Clone this repo 
- To build a docker image run the command in project folder 'WebAPIWithDocker' `docker build -t  webapiwithdocker .`
- To run the app run command `docker run -it --rm -p 5000:8080 --name container-webapiwithdocker webapiwithdocker:latest`
- Open the browser and hit he URL "http://localhost:5000/weatherforecast"
- For any port related issue - https://andrewlock.net/why-isnt-my-aspnetcore-app-in-docker-working/

## To push the images to Docker Hub
- You should have a docker account
- Login using your username and password with command `docker login`
- After login tag your local image by running the command `docker tag Local_Image_Name DockerHub_Account_Name/Repository_In_LowerCase:tag`
- Then you can push your image by running the command `docker push ockerHub_Account_Name/Repository_In_LowerCase:tag`
- For more refer [Create Repositories and Push a Docker container image to Docker Hub](https://docs.docker.com/docker-hub/repos/create/#push-a-docker-container-image-to-docker-hub)
