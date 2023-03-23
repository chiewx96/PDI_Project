<template>
  <div class="bg-gray-50 px-8">
    <!-- <v-btn @click="torch = torch">+</v-btn>
    <button @click="torch = !torch">Turn on/off flashlight</button> -->
    <v-text-field
      required
      hide-details="auto"
      v-model="decoded_batch_no"
      label="Batch No"
      @keydown.enter.prevent="outbound"
    ></v-text-field>
    <qrcode-stream
      @init="onInit"
      @decode="onDecode"
      :torch="torch"
      :camera="camera"
    ></qrcode-stream>
  </div>
</template>

<script>
import { QrcodeStream } from 'vue3-qrcode-reader';
import ApiService from '@/services/api.service';
import Swal from 'sweetalert2';

export default {
  components: { QrcodeStream },
  data() {
    return {
      error: '',
      decoded_batch_no: '',
      torch: false,
      fetchData: '',
      camera: 'auto',
    };
  },
  methods: {
    async onInit(promise) {
      try {
        await promise;
      } catch (error) {
        if (error.name === 'NotAllowedError') {
          this.error = 'ERROR: you need to grant camera access permission';
        } else if (error.name === 'NotFoundError') {
          this.error = 'ERROR: no camera on this device';
        } else if (error.name === 'NotSupportedError') {
          this.error = 'ERROR: secure context required (HTTPS, localhost)';
        } else if (error.name === 'NotReadableError') {
          this.error = 'ERROR: is the camera already in use?';
        } else if (error.name === 'OverconstrainedError') {
          this.error = 'ERROR: installed cameras are not suitable';
        } else if (error.name === 'StreamApiNotSupportedError') {
          this.error = 'ERROR: Stream API is not supported in this browser';
        } else if (error.name === 'InsecureContextError') {
          this.error =
            'ERROR: Camera access is only permitted in secure context. Use HTTPS or localhost rather than HTTP.';
        } else {
          this.error = `ERROR: Camera error (${error.name})`;
        }
      } finally {
        this.showScanConfirmation = this.camera === 'off';
      }
    },
    async onDecode(result) {
      this.decoded_batch_no = result;
      this.outbound();
    },
    async testFunc(txt) {
      this.onDecode(txt);
    },
    async showBundleDetails() {
      let tableContent =
        '<v-dialog v-model="dialog" activator="parent" width="auto">' +
        '<v-card>' +
        '<v-container>' +
        '<v-table>' +
        '<thead>' +
        '<tr>' +
        '<th class="text-left">Reference No</th>' +
        '<th class="text-left">Gross Weight</th>' +
        '<th class="text-left">Tare Weight</th>' +
        '<th class="text-left">Nett Weight</th>' +
        '<th class="text-left">Incoming Date Time</th>' +
        '<th class="text-left">Created At</th>' +
        '</tr>' +
        '</thead>' +
        '<tbody>' +
        '<tr>' +
        `<td>${this.fetchData.BatchNo}</td>` +
        `<td>${this.fetchData.GrossWeight}</td>` +
        `<td>${this.fetchData.TareWeight}</td>` +
        `<td>${this.fetchData.NettWeight}</td>` +
        `<td>${this.fetchData.IncomingDateTime}</td>` +
        `<td>${this.fetchData.CreatedAt}</td>` +
        '</tr>' +
        '</tbody>' +
        '</v-table>' +
        '</v-container>' +
        '</v-card>';
      Swal.fire({
        title: 'Bundle Information',
        html: tableContent,
        showCloseButton: true,
        showCancelButton: true,
        allowOutsideClick: false,
        focusConfirm: false,
        confirmButtonText: '<i class="fa fa-check"></i> Confirm',
        cancelButtonText: '<i class="fa fa-times"></i> Cancel',
      }).then((result) => {
        if (result.isConfirmed) {
          Swal.fire(
            'Outbound!',
            'This item is registered for outbound.',
            'success'
          );
          result.dismiss === Swal.DismissReason.close;
        } else {
          result.dismiss === Swal.DismissReason.cancel;
        }
        this.reloadPage();
      });
    },
    async reloadPage() {
      await this.timeout(500);
      window.location.reload();
    },
    outbound() {
      if (this.decoded_batch_no != '') {
        ApiService._get('outbound/outbound/'+ this.decoded_batch_no)
          .then(async (response) => {
            if (response.status == 200) {
              Swal.fire({
                icon: 'success',
                title: 'Outbound success.',
                text: 'Batch No : ' + this.decoded_batch_no,
              });
              this.decoded_batch_no = '';
            } else {
              Swal.fire({
                icon: 'error',
                title: 'Outbound Error.',
                text: 'Contact Administrator! Outbound process unavailable.',
              });
            }
          })
          .catch(() => {
            Swal.fire({
              icon: 'error',
              title: 'Network Error. Could not connect to api service.',
              text: 'Contact Administrator!',
            });
          });
      }
    },
    timeout(ms) {
      return new Promise((resolve) => {
        window.setTimeout(resolve, ms);
      });
    },
  },
};
</script>
<style scoped>
.scan-confirmation {
  position: absolute;
  width: 100%;
  height: 100%;

  background-color: rgba(255, 255, 255, 0.8);

  display: flex;
  flex-flow: row nowrap;
  justify-content: center;
}
</style>
