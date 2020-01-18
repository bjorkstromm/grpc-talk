// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: DemoService.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Demot {
  public static partial class DemoService
  {
    static readonly string __ServiceName = "demot.DemoService";

    static readonly grpc::Marshaller<global::Demot.Request> __Marshaller_demot_Request = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Demot.Request.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Demot.Reply> __Marshaller_demot_Reply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Demot.Reply.Parser.ParseFrom);

    static readonly grpc::Method<global::Demot.Request, global::Demot.Reply> __Method_Unary = new grpc::Method<global::Demot.Request, global::Demot.Reply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Unary",
        __Marshaller_demot_Request,
        __Marshaller_demot_Reply);

    static readonly grpc::Method<global::Demot.Request, global::Demot.Reply> __Method_ServerStreaming = new grpc::Method<global::Demot.Request, global::Demot.Reply>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ServerStreaming",
        __Marshaller_demot_Request,
        __Marshaller_demot_Reply);

    static readonly grpc::Method<global::Demot.Request, global::Demot.Reply> __Method_ClientStreaming = new grpc::Method<global::Demot.Request, global::Demot.Reply>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "ClientStreaming",
        __Marshaller_demot_Request,
        __Marshaller_demot_Reply);

    static readonly grpc::Method<global::Demot.Request, global::Demot.Reply> __Method_BidirectionalStreaming = new grpc::Method<global::Demot.Request, global::Demot.Reply>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "BidirectionalStreaming",
        __Marshaller_demot_Request,
        __Marshaller_demot_Reply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Demot.DemoServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of DemoService</summary>
    [grpc::BindServiceMethod(typeof(DemoService), "BindService")]
    public abstract partial class DemoServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Demot.Reply> Unary(global::Demot.Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task ServerStreaming(global::Demot.Request request, grpc::IServerStreamWriter<global::Demot.Reply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Demot.Reply> ClientStreaming(grpc::IAsyncStreamReader<global::Demot.Request> requestStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task BidirectionalStreaming(grpc::IAsyncStreamReader<global::Demot.Request> requestStream, grpc::IServerStreamWriter<global::Demot.Reply> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for DemoService</summary>
    public partial class DemoServiceClient : grpc::ClientBase<DemoServiceClient>
    {
      /// <summary>Creates a new client for DemoService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public DemoServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for DemoService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public DemoServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected DemoServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected DemoServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Demot.Reply Unary(global::Demot.Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Unary(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Demot.Reply Unary(global::Demot.Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Unary, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Demot.Reply> UnaryAsync(global::Demot.Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UnaryAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Demot.Reply> UnaryAsync(global::Demot.Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Unary, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::Demot.Reply> ServerStreaming(global::Demot.Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ServerStreaming(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Demot.Reply> ServerStreaming(global::Demot.Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ServerStreaming, null, options, request);
      }
      public virtual grpc::AsyncClientStreamingCall<global::Demot.Request, global::Demot.Reply> ClientStreaming(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ClientStreaming(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncClientStreamingCall<global::Demot.Request, global::Demot.Reply> ClientStreaming(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_ClientStreaming, null, options);
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Demot.Request, global::Demot.Reply> BidirectionalStreaming(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return BidirectionalStreaming(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Demot.Request, global::Demot.Reply> BidirectionalStreaming(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_BidirectionalStreaming, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override DemoServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new DemoServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(DemoServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Unary, serviceImpl.Unary)
          .AddMethod(__Method_ServerStreaming, serviceImpl.ServerStreaming)
          .AddMethod(__Method_ClientStreaming, serviceImpl.ClientStreaming)
          .AddMethod(__Method_BidirectionalStreaming, serviceImpl.BidirectionalStreaming).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, DemoServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Unary, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Demot.Request, global::Demot.Reply>(serviceImpl.Unary));
      serviceBinder.AddMethod(__Method_ServerStreaming, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Demot.Request, global::Demot.Reply>(serviceImpl.ServerStreaming));
      serviceBinder.AddMethod(__Method_ClientStreaming, serviceImpl == null ? null : new grpc::ClientStreamingServerMethod<global::Demot.Request, global::Demot.Reply>(serviceImpl.ClientStreaming));
      serviceBinder.AddMethod(__Method_BidirectionalStreaming, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Demot.Request, global::Demot.Reply>(serviceImpl.BidirectionalStreaming));
    }

  }
}
#endregion
