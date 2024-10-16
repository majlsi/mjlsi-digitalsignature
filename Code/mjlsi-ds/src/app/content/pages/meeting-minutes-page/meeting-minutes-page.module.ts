import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeetingMinutesPageComponent } from './meeting-minutes-page.component';
import { Routes, RouterModule } from '@angular/router';
import { MinutesOfMeetingComponent } from './minutes-of-meeting/minutes-of-meeting.component';
import { MeetingMinutesModule } from '../../components/meeting-minutes/meeting-minutes.module';
const routes: Routes = [
	{
		path: 'document',
		component: MeetingMinutesPageComponent,
		children: [

			{
				path: '',
				component: MinutesOfMeetingComponent,

			}

		]
	}
];
@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MeetingMinutesModule
  ],
  declarations: [MeetingMinutesPageComponent, MinutesOfMeetingComponent]
})


export class MeetingMinutesPageModule { }
