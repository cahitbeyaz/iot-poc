import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { error } from 'util';

@Component({
  selector: 'app-lock-management',
  templateUrl: './lock-management.component.html'
})
export class LockManagementComponent {
  public lockDeviceEventResults: Observable<LockDeviceEventResult[]>;
  baseuri: string;

  public ChangeStatus(lockDevice: LockDeviceEventResult, @Inject('BASE_URL') baseUrl: string) {
    this.http.post(this.baseuri + 'api/Lock/UpdateLockStatus', lockDevice).subscribe(result => {
    }, error => {
      console.error(error);
      lockDevice.lockDevice.isActive = !lockDevice.lockDevice.isActive;
    });

  }

  constructor(public http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseuri = baseUrl;
    http.get<Observable<LockDeviceEventResult[]>>(baseUrl + 'api/Lock/LockEvents').subscribe(result => {
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
