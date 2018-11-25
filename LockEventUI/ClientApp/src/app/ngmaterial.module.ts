import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';


@NgModule({
  imports: [MatButtonModule, MatSlideToggleModule],
  exports: [MatButtonModule, MatSlideToggleModule]
})
export class MaterialAppModule { }
