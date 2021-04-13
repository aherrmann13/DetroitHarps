import { Inject, Component } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';

export interface DeletePromptDialogInput {
  onClick: () => Observable<void>;
  itemName: string;
}

@Component({
  selector: 'dh-admin-delete-prompt-modal',
  templateUrl: 'delete-prompt.component.html'
})
export class DeletePromptDialogComponent {
  loading = false;
  itemName: string;

  constructor(
    private _dialogRef: MatDialogRef<DeletePromptDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private _data: DeletePromptDialogInput
  ) {
    this.itemName = _data.itemName;
  }

  onYesClick() {
    this.loading = true;
    this._data.onClick().subscribe(
      _ => this.handleSuccess(),
      err => this.handleError(err)
    );
  }
  onNoClick() {
    this._dialogRef.close({ yes: false });
  }

  private handleSuccess(): void {
    this.loading = false;
    this._dialogRef.close({ yes: true });
  }

  private handleError(err: any): void {
    this.loading = false;
    console.error(err);
    this._dialogRef.close({ yes: false });
  }
}
