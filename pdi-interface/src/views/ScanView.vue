<template>
  <div class="bg-gray-50 px-8">
    <p>{{ error }}</p>
    <p>{{ decodedString }}</p>
    <button @click="torch = !torch">Turn on/off flashlight</button>
    <qrcode-stream
      @init="onInit"
      @decode="onDecode"
      :torch="torch"
    ></qrcode-stream>
    <p>{{ fetchData }}</p>
  </div>
</template>

<script>
import { QrcodeStream } from "vue3-qrcode-reader";
import ApiService from "@/components/ApiService.vue";

export default {
  components: { QrcodeStream },
  data() {
    return {
      error: "",
      decodedString: "",
      torch: false,
      fetchData: "",
    };
  },
  mounted() {
    this.testFunc("B23250A00008");
  },
  methods: {
    async onInit(promise) {
      try {
        await promise;
      } catch (error) {
        if (error.name === "NotAllowedError") {
          this.error = "ERROR: you need to grant camera access permission";
        } else if (error.name === "NotFoundError") {
          this.error = "ERROR: no camera on this device";
        } else if (error.name === "NotSupportedError") {
          this.error = "ERROR: secure context required (HTTPS, localhost)";
        } else if (error.name === "NotReadableError") {
          this.error = "ERROR: is the camera already in use?";
        } else if (error.name === "OverconstrainedError") {
          this.error = "ERROR: installed cameras are not suitable";
        } else if (error.name === "StreamApiNotSupportedError") {
          this.error = "ERROR: Stream API is not supported in this browser";
        } else if (error.name === "InsecureContextError") {
          this.error =
            "ERROR: Camera access is only permitted in secure context. Use HTTPS or localhost rather than HTTP.";
        } else {
          this.error = `ERROR: Camera error (${error.name})`;
        }
      }
    },
    onDecode(result) {
      this.decodedString = result;
      var obj = JSON.stringify(result);
      ApiService._get("/outbound/get-details/" + obj.batch_no).then(
        (response) => {
          if (response.status.code == 200) {
            this.fetchData = response.status.result;
          }
        }
      );
    },
    async testFunc(txt) {
      // var obj = JSON.parse(txt);
      // fetch(
      //   process.env.VUE_APP_BASEURL +
      //     "/api/outbound/get-details/" +
      //     obj
      // ).then((response) => (this.fetchData = response.json()));
      let result = await ApiService._get("outbound/get-details/" + txt);
      console.log(result);
    },
  },
};
</script>
