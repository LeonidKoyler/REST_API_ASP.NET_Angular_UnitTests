import { Component, OnInit } from '@angular/core';

import { Vehicle } from './vehicle.model';
import { VehicleService } from './vehicle.service';
@Component({
  selector: 'pm-calculation',
  templateUrl: './calculation.component.html',
})
export class CalculationComponent implements OnInit {
  private inputValue: number;
  vechileType: string[] = ['Common', 'Luxury'];
  vehicleData: Vehicle[] = [];
  selectedVehicleType: string;

  constructor(private service: VehicleService) {}

  ngOnInit(): void {
    this.selectedVehicleType = this.vechileType[0];
  }

  
  // Can be async but for the current function is not relevant
  // Errors not implemented
  calculate_price() {
    const vehicle: Vehicle = {
      basePrice: this.inputValue,
      type: this.selectedVehicleType,
    };

    this.service.calculate(vehicle).subscribe(
      (data: Vehicle) => {
          const vehicle_price: Vehicle = {
            basePrice: data.basePrice,
            type: data.type,
            basicBuyerFee: data.basicBuyerFee,
            sellersSpecialFee: data.sellersSpecialFee,
            associationCost: data.associationCost,
            storageFee: data.storageFee,
            totalCost: data.totalCost,
          };
      this.vehicleData.push(vehicle_price);
    },);
  }

  set updateInput(value: string) {
    const parsedValue = parseInt(value, 10);
    if (!isNaN(parsedValue) && parsedValue > 0) {
      this.inputValue = parsedValue;
      this.calculate_price();
    }
  }

  onSelectionChange(event: any): void {
    if (this.inputValue == undefined) {
      return;
    }

    this.calculate_price();
  }
}
