import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Date } from 'src/app/components/_util/Date';

@Pipe({
  name: 'DateFormat'
})
export class DateFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Date.Date_Time);
  }

}
