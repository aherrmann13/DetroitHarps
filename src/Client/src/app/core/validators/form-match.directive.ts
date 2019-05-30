import { ValidatorFn, AbstractControl } from '@angular/forms';

export const FORM_MATCH_ERROR = 'formMatchError';

export function formMatchValidator(regex: RegExp): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const allowed = regex.test(control.value);
    return allowed ? null : { formMatchError: { value: control.value } };
  };
}
