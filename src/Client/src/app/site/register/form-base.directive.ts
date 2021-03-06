import { Input, OnInit, Directive } from '@angular/core';
import { RegisterModel } from '../../core/client/api.client';
import { AbstractControl, FormBuilder } from '@angular/forms';

@Directive()
export abstract class FormBaseDirective implements OnInit {
  @Input() registration: RegisterModel;

  constructor(protected formBuilder: FormBuilder) {
    this.buildControl();
  }

  ngOnInit(): void {
    this.control.valueChanges.subscribe(x => {
      if (this.isControlValid()) {
        this.updateModel();
      }
    });
  }

  abstract get control(): AbstractControl;

  protected abstract buildControl(): void;

  protected abstract updateModel(): void;

  protected isControlValid(): boolean {
    return this.control.valid;
  }
}
