<template>
  <div>
    <div class="small">
      <distance-chart :chart-data="datacollection"></distance-chart>
    </div>
  </div>
</template>

<script>
import DistanceChart from "./DistanceChart";
import { mapActions, mapState } from "vuex";

export default {
  name: "DistanceStatus",
  components: {
    DistanceChart
  },
  data() {
    return {
    };
  },
  mounted() {
    //this.fillData()
    this.getAllStatus();
  },
  methods: {
    ...mapActions("status", {
      getAllStatus: "getAll"
    })
  },
  computed: {
    ...mapState({
      statuses: state => state.status.all.items
    }),
    datacollection: function() {
      return {
        labels: ["Position"],
        datasets: (this.statuses || []).map((status, i) => {
          return {
            label: status.robot_name,
            backgroundColor: i % 2 === 0 ? "#996666" : "#666699",
            data: [
              { x: "x", y: status.position.x },
              { x: "y", y: status.position.y }
            ]
          };
        })
      };
    }
  }
};
</script>

<style scoped>
.small {
  max-width: 700px;
  height: 90px;
  margin: 100px auto;
}
</style>