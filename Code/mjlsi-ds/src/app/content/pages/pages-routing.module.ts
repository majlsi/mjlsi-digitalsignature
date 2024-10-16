
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';
import { NgxPermissionsGuard } from 'ngx-permissions';


const routes: Routes = [
	{
		path: '',
		component: PagesComponent,
		children: [
			{
				path: '',
				loadChildren: './meeting-minutes-page/meeting-minutes-page.module#MeetingMinutesPageModule'
			}
		]
	}

];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class PagesRoutingModule {}
