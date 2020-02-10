import { NgModule } from '@angular/core';
import { SharedCommonModule } from './modules/shared-modules/shared-common.module';
import { SharedComponentsModule } from './modules/shared-modules/shared-components.module';
import { SharedServiceModule } from './modules/shared-modules/shared-services.module';
import { NgMaterialModule } from './modules/vendor/ng-material.module';
import { SharedSnackbarComponent } from './modals/shared-snackbar/shared-snackbar.component';
import { NgIconsModule } from './modules/vendor/ng-icons.module';

@NgModule({
  declarations: [SharedSnackbarComponent],
  imports: [
    // The general common module used by the application
    SharedCommonModule,

    // The shared module tha contains all the services
    SharedServiceModule,

    // All shared components should be stored here
    SharedComponentsModule,

    // Angular material module
    NgMaterialModule,

    // Import all the icons to be used by the application
    NgIconsModule,
  ],

  exports: [
    SharedServiceModule,
    SharedCommonModule,
    SharedComponentsModule,
    SharedComponentsModule,
    NgMaterialModule,
    NgIconsModule,
  ],

  // Modals used by all should be stored here.
  entryComponents: [],

  providers: [
  ]
})


/**
 * @description Shared module contains all the module only used by all other modules
 */
export class SharedModule { }
