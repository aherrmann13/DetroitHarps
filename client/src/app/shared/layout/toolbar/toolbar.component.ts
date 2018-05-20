import { Component, Input, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'dh-toolbar',
  templateUrl : './toolbar.component.html',
  styleUrls: [ './toolbar.component.scss' ]
})
export class ToolbarComponent {
  @Input() title: string;
  @Input() hideMenuButton:  boolean;

  @Output() menuClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  private handleClick(): void {
    this.menuClick.emit(true);
  }
}
