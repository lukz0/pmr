<template>
    <div>
        <div class="small">
            <line-chart :chart-data="datacollection"></line-chart>
        </div>
    </div>
</template>

<script>
    import LineChart from './LineChart'

    export default {
        name: "Status",
        components: {
            LineChart
        },
        data() {
            return {
                datacollection: {
                    labels: [],
                    datasets: []
                }
            }
        },
        mounted() {
            //this.fillData()
        },
        methods: {
            /*fillData() {
                this.datacollection = {
                    labels: [1, 2, 3, 4],
                    datasets: [
                        {
                            label: 'Robot3X-323',
                            backgroundColor: '#007a98',
                            data: [0, 50, 30, 140, 60]
                        }, {
                            label: 'MILRXX293',
                            backgroundColor: '#dac7ff',
                            data: [this.getRandomInt(), this.getRandomInt(), this.getRandomInt(), 70]
                        },
                        {
                            label: 'Rob4344-SS',
                            backgroundColor: '#ffe744',
                            data: [this.getRandomInt(), this.getRandomInt()+3, 52, 68]
                        }
                    ]
                }
            },
            getRandomInt() {
                return Math.floor(Math.random() * (50 - 5 + 1)) + 5
            }*/
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