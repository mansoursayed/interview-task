import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class FileUploadService {
  private readProgressSubject: ReplaySubject<number> = new ReplaySubject<number>();
  private fileContentSubject: ReplaySubject<string> = new ReplaySubject<string>();
  private fileErrorSubject: ReplaySubject<string> = new ReplaySubject<string>();

  get getFileError() {
    return this.fileErrorSubject.asObservable();
  }

  get getReadProgress() {
    return this.readProgressSubject.asObservable();
  }

  get getFileContent() {
    return this.fileContentSubject.asObservable();
  }
  constructor() { }

  readDocument(file: File) {
    let fileReader = new FileReader();
    fileReader.onload = (e) => {
      let content: string = fileReader.result as string;
      let redundentWords = content.split(" ").filter((item, index, array) => item.trim() && array.indexOf(item) !== index);
      let originalContent = "File content is: " + content;
      let filename = "File Name  is: " + file.name;
      let redudentContent: string = "";
      if (redundentWords.length > 0) {
        redudentContent = "Redundent content is: " + redundentWords.join(",");
      }
      else {
        redudentContent = "No redundent content";
      }
      this.fileContentSubject.next(filename+ ' <br> ' + originalContent + ' <br> ' + redudentContent);

    }
    fileReader.readAsText(file);

    fileReader.onprogress = (event) => {
      if (event.lengthComputable) {
        var progress = parseInt(((event.loaded / event.total) * 100).toString(), 10);
        if (!Number.isNaN(progress)) {
          this.readProgressSubject.next(progress);
        }
      }
    };
    fileReader.onerror = function () {
      throw new Error("There was an issue reading the file." + fileReader.error);
    };

  }
}
