import { BrowserModule, HammerModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';

import { OverlayModule } from '@angular/cdk/overlay';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserAnimationsModule, BrowserModule, OverlayModule, CoreModule, HammerModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
