#### Linting

Followed instructions [here](https://github.com/angular-eslint/angular-eslint) to use eslint and added prettier config

#### Unit testing

unfortunatly, when this site was first written I didn't realize the value of frontend unit tests, and skipped out on adding these

angular docs recommend using jasmine/karma for unit tests: [source](https://angular.io/guide/testing), but im going to use jest as thats what im familiar with.

references:
https://itnext.io/angular-testing-series-how-to-add-jest-to-angular-project-smoothly-afffd77cc1cb

https://codeburst.io/angular-6-ng-test-with-jest-in-3-minutes-b1fe5ed3417c?gi=b77ee33c5300
https://github.com/just-jeb/angular-builders/tree/master/packages/jest

#### Misc Unit Test Notes

source for overlay container magic in unit tests: https://github.com/angular/components/blob/master/src/material/select/select.spec.ts

the overlay part makes sense, the 'inject' stuff is still a little magic

TODO: complete this summary of how overlay + inject works

TODO: test datepicker updates input value
