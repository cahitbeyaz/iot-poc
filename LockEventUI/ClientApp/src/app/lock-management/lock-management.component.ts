import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-lock-management',
  templateUrl: './lock-management.component.html'
})
export class LockManagementComponent {
  public lockDeviceEventResults: LockDeviceEventResult[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<LockDeviceEventResult[]>(baseUrl + 'api/Lock/LockEvents').subscribe(result => {
      this.lockDeviceEventResults = result;
    }, error => console.error(error));
  }
}

interface LockDevice {
  lockDeviceId: string;
  isActive: boolean;
  lastActiveTime: Date;
}

interface LockDeviceEvent {
  eventTime: Date;
  requestReferenceNumber: string;
  deviceEvent: string;
}

interface LockDeviceEventResult {
  lockDevice: LockDevice;
  lockDeviceEvents: LockDeviceEvent[];
}
