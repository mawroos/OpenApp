import Vue from 'vue'
import Component from 'vue-class-component'
import { DxChart, DxSeries } from 'devextreme-vue/chart';
import DataSource from 'devextreme/data/data_source';
import CustomStore from 'devextreme/data/custom_store';



@Component({   
    props: {
        propMessage: String
    },
    components: {
        DxChart,
       
        DxSeries
        
    }
})
export default class App extends Vue {
    // props have to be declared for typescript
    propMessage: string;

    // inital data
    greeting: string = '';
    msg: number = 123;
    
    get helloMsg() {
        return 'Hello, ' + this.propMessage + ': ' + this.greeting;
    }

    // lifecycle hook
    mounted() {
        this.greet();
    }

    // computed
    get computedMsg() {
        return 'computed ' + this.msg;
    }

    // method
    async greet() {

        this.greeting = "Hi from typescript";

    }




    async GetData() {
        var dimensions = [JSON.stringify({ Name: "Co" }), JSON.stringify({ Name: "SellingLocation" })];
        var measures = [JSON.stringify({ Name: "Sales", ColumnName: "NetSales", AggregationType: 0 })];
        var input = {
            dimensions: dimensions,
            measures: measures
        }
        var results = await abp.services.app.demo1Service.getData2(input);
        return results;
    }

    PostData() {
        var dimensions = [{ Name: "Co" },{ Name: "SellingLocation" }];
        var measures = [{ Name: "Sales", ColumnName: "NetSales", AggregationType: 0 }];
        var input = {
            dimensionColumns: dimensions,
            measureColumns: measures
        };
        var results = abp.services.app.demo1Service.requestData2(input);
        return results;
    }

    populationData = new DataSource({
        store: new CustomStore({
            load: () => {
                return this.PostData();
            },
            loadMode: 'raw'
        }),
        //filter: ['t', '>', '6'],
        paginate: false
    });
}
