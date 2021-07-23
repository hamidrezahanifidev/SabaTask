import { Pipe, PipeTransform } from "@angular/core";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";

@Pipe({name: 'safeImagePath'})
export class SafeImagePathPipe implements PipeTransform {

  constructor(private sanitizer: DomSanitizer){}

  transform(url): SafeUrl{
    return this.sanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + url)
  }
}
