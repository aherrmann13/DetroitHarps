#### Overview

auth0 settings are configured in the `environment.ts` file

structure is 
```
auth0: {
    clientID: string,
    domain: string,
    audience: string,
    redirectUri: string
}
```


clientId:
  the client id of the application that is configured in auth0 that will be making requests
domain:
  corresponds to the auth0 tenant being used.  pattern is {tenant}.auth0.com
audience:
  the audience is the api being called.  this value must match the audience of an api in auth0, it is not enough to add the url of the api.  for example, http://localhost:5000 cannot be used unless there is an api added in auth0 for local development
redirectUri
  this is the callback url that is returned by auth0 after login.  to prevent this value from being manipulated by unauthorized parties, this value is returned only if the value appears in the Allowed Callback URL field in the calling application's auth0 configuration


#### General Notes

api currently has no auth0 configured for local development.  authentication should be commented out on local right now or dev environment should be used.

if this becomes an inconvenience, localhost can be set up as an api in auth0 as well