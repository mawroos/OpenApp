import Vue from 'vue'
import Component from 'vue-class-component'

@Component({   
    props: {
        propMessage: String
    },
    components: {
        
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
        var dimensions = [JSON.stringify({ Name: "Co" }), JSON.stringify({ Name: "SellingLocation" })];
        var measures = [JSON.stringify({ Name: "Sales", ColumnName: "NetSales", AggregationType: 0 })];
        var input = {
            dimensions: dimensions,
            measures: measures
        }
        var results = await abp.services.app.demo1Service.getData2(input);
        this.greeting = JSON.stringify(results);

    }

}
