import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FileUploadService } from '../../services/file-upload.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {
  EmptyFileMessage = 'Select File';
  currentFile?: File;
  progress = 0;
  message = '';
  fileContent?: string = "";
  fileName = this.EmptyFileMessage;
  @ViewChild('fileInput') fileInput!: ElementRef;
  constructor(private uploadService: FileUploadService) {
    uploadService.getFileContent.subscribe(content => {
      this.fileContent = content;
    });

    uploadService.getReadProgress.subscribe(progress => {
      this.progress = progress;
    });
    uploadService.getFileError.subscribe(error => {
      this.message = error;
    });
  }
  ngOnInit(): void {

  }

  selectFile(event: any): void {
    this.message = '';
    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      if (file.type == 'text/plain') {
        this.currentFile = file;
        this.fileName = this.currentFile.name;
        this.read();
      }
      else {
        this.message = "File type is not supported";
      }
    } else {
      this.fileName = 'Select File';
    }
  }

  read(): void {
    this.progress = 0;
    this.message = "";
    if (this.currentFile) {
      this.uploadService.readDocument(this.currentFile);
      this.fileInput.nativeElement.value = '';
      this.fileName = this.EmptyFileMessage;
    }

  }

}
