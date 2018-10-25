import { InMemoryDbService } from 'angular-in-memory-web-api';
import { TaskTemplate } from '../models/task-template';
import { RequestInfo, ResponseOptions } from 'angular-in-memory-web-api';

export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const taskTemplates = [
      { id: 11, name: 'Staubsaugen', intervalInDays: 2, reward: 1 },
      { id: 12, name: 'Staubsaugen', intervalInDays: 2, reward: 1 },
      { id: 13, name: 'Staubsaugen', intervalInDays: 2, reward: 1 },
      { id: 14, name: 'Staubsaugen', intervalInDays: 2, reward: 1 }
    ];
    return {TaskTemplate: taskTemplates};
  }

  // intercept ResponseOptions from default HTTP method handlers
  // add a response header and report interception to console.log
  responseInterceptor(resOptions: ResponseOptions, reqInfo: RequestInfo) {
    const method = reqInfo.method.toUpperCase();
    console.log(`request intercepted by the InMemoryDataService: ${method} ${reqInfo.req.url}`);
    return resOptions;
  }

  //returns highest id
  genId(TaskTemplate: TaskTemplate[]): number {
    return TaskTemplate.length > 0 ? Math.max(...TaskTemplate.map(tt => tt.id)) + 1 : 11;
  }
}
