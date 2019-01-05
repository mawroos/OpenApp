<template>
    <div class="hello">
        <h1>{{ helloMsg }}</h1>
        <dx-chart id="chart"
                  :data-source="populationData">
            <dx-series argument-field="Co"
                       value-field="Sales"
                       name="Company Sales"
                       type="bar"
                       color="#ffaa66" />
        </dx-chart>


    </div>


</template>

<script lang="ts" src="./DemoTypescript.vue.ts"></script>
<style scoped>
    .options {
        padding: 20px;
        background-color: rgba(191, 191, 191, 0.15);
        margin-top: 20px;
    }

    .option {
        margin-top: 10px;
    }

    .caption {
        font-size: 18px;
        font-weight: 500;
    }

    .option > span {
        margin-right: 10px;
    }

    .option > .dx-widget {
        display: inline-block;
        vertical-align: middle;
    }
</style>
