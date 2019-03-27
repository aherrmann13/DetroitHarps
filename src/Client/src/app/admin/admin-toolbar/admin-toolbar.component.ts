import { Component, Input } from '@angular/core';

export interface AdminToolbarItem {
    icon: string;
    onClick: () => void;
}

@Component({
  selector: 'dh-admin-toolbar',
  templateUrl: 'admin-toolbar.component.html',
  styleUrls: [ '../admin.component.scss' ]
})
export class AdminToolbarComponent {
    @Input() items: Array<AdminToolbarItem>;
}