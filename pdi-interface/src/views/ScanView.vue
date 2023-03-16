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
    <!-- <v-button @click="() => (bundle_info_toggle = true)">Test </v-button> -->
    <!-- <BundleInfo
      @confirm_outbound="confirm_outbound()"
      @cancel_outbound="cancel_outbound()"
      :bundle_info_toggle="bundle_info_toggle"
      :bundle_info="bundle_info"
    /> -->
    <v-container>
      <v-table v-show="fetchData != null">
        <thead>
          <tr>
            <th class="text-left">Reference No</th>
            <th class="text-left">Gross Weight</th>
            <th class="text-left">Tare Weight</th>
            <th class="text-left">Nett Weight</th>
            <th class="text-left">Incoming Date Time</th>
            <th class="text-left">Created At</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{{ fetchData != null ? fetchData.BatchNo : "" }}</td>
            <td>{{ fetchData != null ? fetchData.GrossWeight : "" }}</td>
            <td>{{ fetchData != null ? fetchData.TareWeight : "" }}</td>
            <td>{{ fetchData != null ? fetchData.NettWeight : "" }}</td>
            <td>{{ fetchData != null ? fetchData.IncomingDateTime : "" }}</td>
            <td>{{ fetchData != null ? fetchData.CreatedAt : "" }}</td>
          </tr>
        </tbody>
      </v-table>
      <v-row>
        <v-col>
          <v-btn color="primary" block @click="confirm_outbound">Confirm</v-btn>
        </v-col>
        <v-col>
          <v-btn color="danger" block @click="cancel_outbound">Close</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
import { QrcodeStream } from "vue3-qrcode-reader";
import ApiService from "@/components/ApiService.js";
// import BundleInfo from '@/components/BundleInfo.vue';

export default {
  components: { QrcodeStream },
  data() {
    return {
      error: "",
      decodedString: "",
      torch: false,
      fetchData: null,
      // bundle_info: '',
      // bundle_info_toggle: false,
    };
  },
  mounted() {
    // this.testFunc(
    //   `{"batch_no":"C23251A00037","gross_weight":"251.54","tare_weight":"10.00","incoming_date":"2023-03-11 22:29:23"}`
    // );
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
      var obj = JSON.parse(result);
      this.decodedString = obj;
      ApiService._get("outbound/get-details/" + obj.batch_no).then(
        async (response) => {
          if (response.status == 200) {
            this.fetchData = await response.json();
          }
        }
      );
    },
    async testFunc(txt) {
      this.onDecode(txt);
    },
    confirm_outbound() {
      this.fetchData = null;
      this.decodedString = null;
      // this.bundle_info_toggle = false;
    },
    cancel_outbound() {
      this.fetchData = null;
      this.decodedString = null;
      // this.bundle_info_toggle = false;
    },
  },
};
</script>
