import { Component } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Configurations } from '../../../configs/config';
import { ToastrService } from 'ngx-toastr';
import { CanComponentDeactivate } from '../../../shared/models/canComponentDeactivate ';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrl: './upload-photo.component.css'
})
export class UploadPhotoComponent  implements CanComponentDeactivate{
  selectedFile: File | null = null;
  photoPreview: string | ArrayBuffer | null = null;
  uploadProgress: number = 0;
  uploadSuccess: boolean = false;
  isUploading: boolean = false;
  constructor(private httpClient: HttpClient ,private toastr:ToastrService) {}

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      this.selectedFile = input.files[0];

      // Create a photo preview
      const reader = new FileReader();
      reader.onload = () => {
        this.photoPreview = reader.result;
      };
      reader.readAsDataURL(this.selectedFile);
    }
  }

  uploadPhoto(): void {
    if (!this.selectedFile) {
      return;
    }

    const uploadUrl = `${environment.apiUrlAuth}${Configurations.Account.UploadPhoto}`; // Replace with your upload API endpoint
    const formData = new FormData();
    formData.append('file', this.selectedFile, this.selectedFile.name);

    this.isUploading = true;

    this.httpClient.post(uploadUrl, formData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe(
      event => {
        if (event.type === HttpEventType.UploadProgress && event.total) {
          this.uploadProgress = Math.round((100 * event.loaded) / event.total);
        } else if (event.type === HttpEventType.Response) {
          this.isUploading = false;
          this.uploadSuccess = true;
          this.toastr.success('Photo uploaded successfully!');
        }
      },
      error => {
        this.isUploading = false;
        this.toastr.error('Upload failed', error);
      }
    );
  }

  canDeactivate(): Observable<boolean> | Promise<boolean> | boolean {
    if (this.isUploading || (this.selectedFile && !this.uploadSuccess)) {
      return confirm('You have an ongoing upload or unsaved changes. Do you really want to leave?');
    }
    return true;
  }
}
