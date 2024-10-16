
import { LayoutModule } from '../layout/layout.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesRoutingModule } from './pages-routing.module';
import { PagesComponent } from './pages.component';
import { PartialsModule } from '../partials/partials.module';
import { CoreModule } from '../../core/core.module';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { DeleteEntityDialogComponent } from '../partials/content/general/modals/delete-entity-dialog/delete-entity-dialog.component';
import { ActionNotificationComponent} from '../partials/content/general/action-natification/action-notification.component';
import { AlertComponent} from '../partials/content/general/alert/alert.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LayoutUtilsService } from '../../core/services/layout-utils.service';
@NgModule({
	declarations: [
		PagesComponent,
		ErrorPageComponent,
		DeleteEntityDialogComponent,
		ActionNotificationComponent,
		AlertComponent
	],
	imports: [
		CommonModule,
		HttpClientModule,
		FormsModule,
		PagesRoutingModule,
		CoreModule,
		LayoutModule,
		PartialsModule,
		AngularEditorModule,
		MatProgressBarModule,
		MatDialogModule,
		MatSnackBarModule,
		MatIconModule,
		MatProgressSpinnerModule
	],

	entryComponents: [
		ActionNotificationComponent,
		DeleteEntityDialogComponent,
		AlertComponent
	],

	providers: [LayoutUtilsService]
})
export class PagesModule {}
