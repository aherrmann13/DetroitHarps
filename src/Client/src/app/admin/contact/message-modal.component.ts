import { Inject, Component } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MessageReadModel } from '../../core/client/api.client';

@Component({
  selector: 'dh-admin-message-modal',
  templateUrl: 'message-modal.component.html'
})
export class MessageModalComponent {
  constructor(
    private _dialogRef: MatDialogRef<MessageModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MessageReadModel
  ) {}

  onExitClick() {
    this._dialogRef.close();
  }
}
