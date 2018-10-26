import { observable, configure, action, computed } from 'mobx';
import { WeatherEntry } from "./models/weather-entry";

configure({
  enforceActions: 'always'
});

export class Store {
  @observable private weatherEntries: WeatherEntry[];

  @action
  public setWeatherEntries(weatherEntries: WeatherEntry[]) {
    this.weatherEntries = weatherEntries;
  }

  @computed
  public get orderedWeatherEntries() {
    return this.weatherEntries;
  }
}
