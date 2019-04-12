import { Component, OnInit } from "@angular/core";
import { MatTableDataSource, MatDialog } from "@angular/material";
import { Client, MessageReadModel } from "../../shared/client/api.client";
import { MessageModalComponent } from "./message-modal.component";

@Component({
    selector: 'dh-admin-contact',
    templateUrl: 'contact.component.html',
    styleUrls: [ 'contact.component.scss' ]
})
export class ContactComponent implements OnInit {
    private _messages: Array<MessageReadModel>;
    columnsToDisplay = ['from', 'email', 'timestamp', 'message', 'view' ];
    dataSource = new MatTableDataSource<MessageReadModel>();

    constructor(
        private _dialog: MatDialog,
        private _client: Client) {}

    ngOnInit(){
        this._client.getAllMessages()
            .subscribe(
                data => {
                    this._messages = data;
                    this.refreshDataSource();
                },
                err => console.log(err)
            )
    }

    openMessage(message: MessageReadModel){
        const dialogRef = this._dialog.open(
            MessageModalComponent,
            {
              disableClose: true,
              data: message
            });
      
        dialogRef.beforeClose().subscribe(() => this.markAsRead(message));
    }

    private refreshDataSource(): void{
        this.dataSource.data = this._messages.sort((x, y) => {
            return y.timestamp.getTime() - x.timestamp.getTime()
        });
    }

    private markAsRead(message: MessageReadModel): void {
        if(!message.isRead){
            this.updateTableMessageAsRead(message)
            this._client.markAsRead(message.id)
                .subscribe(
                    () => null,
                    err => {
                        this.updateTableMessageAsUnread(message)
                        console.log(err)
                    }
                )
        }
    }

    private updateTableMessageAsRead(message: MessageReadModel){
        message.isRead = true;
        this.refreshDataSource();
    }

    private updateTableMessageAsUnread(message: MessageReadModel){
        message.isRead = false;
        this.refreshDataSource();
    }
}