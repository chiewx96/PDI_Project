<template>
  <div class="home">
    <v-container class="bg-surface-variant">
      <v-row size="bg" align-content="center" justify>
        <v-col>
          <v-sheet class="ma-10 pa-10">
            <button
              class="inherit"
              v-text="`New Batch`"
              @click="startScan(1)"
            />
          </v-sheet>
          <v-sheet class="ma-10 pa-10">
            <button
              class="inherit"
              v-text="`Continue previous batch`"
              @click="startScan(0)"
            />
          </v-sheet>
        </v-col>
      </v-row>
      <!-- <v-row
        size="bg"
        align-content="center"
        justify
      >
        <v-col>
        </v-col> -->
      <!-- </v-row> -->
    </v-container>
  </div>
</template>

<script>
// import ApiService from "@/components/ApiService";
export default {
  name: "HomeView",
  methods: {
    startScan() {
      // ApiService._post("outbound/")
    },
  },
};
</script>

<style>
v-row {
  margin: 50px;
}
.inherit {
  width: 100%;
  height: 100%;
}
</style>
