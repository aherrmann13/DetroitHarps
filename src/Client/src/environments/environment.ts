// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  apiUrl: 'https://dev.api.detroitharps.com',
  auth0: {
    clientID: 'WCmWyHK0ds6prgFZ3G2NphFwB2nnGTNe',
    domain: 'angband.auth0.com',
    audience: 'https://dev.api.detroitharps.com',
    redirectUri: 'http://localhost:4200/admin/callback'
  }
};
