import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'm-meeting-minutes-page',
  template: '<router-outlet></router-outlet>',
  changeDetection: ChangeDetectionStrategy.Default
})
export class MeetingMinutesPageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}

