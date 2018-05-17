import { Component, Input, EventEmitter, Output} from '@angular/core';

@Component({
  selector: 'app-toolbar',
  templateUrl : './toolbar.component.html',
  styleUrls: [ './toolbar.component.scss' ]
})
export class ToolbarComponent {
  @Input() title: string;
  @Input() hideMenuButton:  boolean;

  @Output() onMenuClick: EventEmitter<boolean> = new EventEmitter<boolean>();

  private handleClick(): void{
    this.onMenuClick.emit(true);
  }
}
