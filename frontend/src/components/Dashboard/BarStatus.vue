<template>
    <div>
        <div class="small">
            <bar-chart :chart-data="datacollection"></bar-chart>
        </div>
    </div>
</template>

<script>
import BarChart from "./BarChart";

export default {
    name: "BarStatus",
    components: {
        BarChart
    },
    data() {
        return {
            datacollection: null
        }
    },
    mounted() {
        this.fillData()
    },
    methods: {
        fillData() {
            this.datacollection = {
                labels: ["",""],
                datasets: [
                    {
                        label: 'Robot3X-323',
                        backgroundColor: '#007a98',
                        data: [12,0]
                    }, {
                        label: 'MILRXX293',
                        backgroundColor: '#dac7ff',
                        data: [20,0]
                    },
                    {
                        label: 'Rob4344-SS',
                        backgroundColor: '#ffe744',
                        data: [11,0]
                    }
                ]
            }
        }
    }
}
</script>

<style scoped>
    .small {
        max-width: 700px;
        height: 90px;
        margin: 100px auto;
    }
</style>