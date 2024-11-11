import { NgModule } from '@angular/core';
import { BootstrapIconsModule } from 'ng-bootstrap-icons';
import { allIcons } from 'ng-bootstrap-icons/icons';



@NgModule({
  declarations: [],
  imports: [
    BootstrapIconsModule.pick(allIcons)
  ],
  exports: [BootstrapIconsModule]
})
export class IconsModule { }
