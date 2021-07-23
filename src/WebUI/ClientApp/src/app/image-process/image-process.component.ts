import { ImagesClient, ImageDto } from './../web-api-client';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackBarComponent } from '../shared/components/snack-bar/snack-bar.component';

@Component({
  selector: 'app-image-process-component',
  templateUrl: './image-process.component.html'
})
export class ImageProcessComponent {
  imgFile: string;
  imageDto: ImageDto[];
  durationInSeconds = 3;

  uploadForm = new FormGroup({
    file: new FormControl('', [Validators.required]),
    imgSrc: new FormControl('', [Validators.required])
  });

  constructor(
    private imagesClient: ImagesClient,
    private _snackBar: MatSnackBar
  ){}

  get uf(){
    return this.uploadForm.controls;
  }

  onImageChange(e) {
    const reader = new FileReader();

    if(e.target.files && e.target.files.length) {
      const [file] = e.target.files;
      reader.readAsDataURL(file);

      reader.onload = () => {
        this.imgFile = reader.result as string;
        this.uploadForm.patchValue({
          imgSrc: reader.result
        });

      };
    }
  }

  upload(){
    this.imagesClient.get(this.uploadForm.value.imgSrc).subscribe(result =>{
        this.imageDto = result.images
      },
      (error) => {
        this._snackBar.openFromComponent(SnackBarComponent, {
          duration: this.durationInSeconds * 1000,
          data: error
        });
      });
  }
}

