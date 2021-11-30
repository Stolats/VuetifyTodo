# Vuetify Todo 

A simple to-do web app deployed to AWS Lambda via github actions, the live version can be found [here](https://2jhl9zb0o6.execute-api.us-east-2.amazonaws.com/Prod/).

Note: The app currently uses runtime memory and is not persistent, due to the nature of the lambda instance only being run on request, lists will be lost after a period of inactivity. There is also currently only a single to-do list exposed, shared by any users at that time.

## Front end

The front end was adapted to use Vue.js 2 with Vuetify components.

## Back end

The back end was built starting from a .Net Core web API tutorial found [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio-code).

## CI
The below files are only used when being run by GitHub actions.

### Related files

- `LambdaEntryPoint.cs`: Wrapper class for ? that serves as the entry point for lambda function. 
- `aws-lambda-tools-defaults.json`: Default values for AWS .Net Core CLI to use.
- `Dockerfile`: Inherits from AWS lambda base image and then copies build artifacts from host machine to the image.
- `serverless.template`: CloudFormation template used by AWS .Net Core CLI command `deploy serverless` found in the GitHub actions deployment script.

## To run

- Create an S3 bucket
- Create an IAM policy as specified
- Create an AWS user
    - Give user the IAM policy
    - Use credentials supplied to create GitHub secrets with ID's as specified in the `CI.yml` file.

# Local Development

From the 'front' directory run `npm run dev`, configured in `/front/package.json`. This creates a full development build and then stays running. Locally saved changes will output files to `/back/wwwroot` as specified in `/front/vue.config.js` which can then be served as usual from the .Net dev build.

This compound configuration is specified as "Server/Client" in launch.json.